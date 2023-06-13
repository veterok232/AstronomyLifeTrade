import React, { useState, useMemo } from "react";
import { Row, Col, Button } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { searchAccessories } from "../../api/catalog/catalogApi";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { ListResult } from "../../dataModels/common/listResult";
import { Pageable } from "../../dataModels/common/pageable";
import { Sortable } from "../../dataModels/common/sortable";
import { SortOrder } from "../../dataModels/enums/sortOrder";
import { isStaff } from "../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getNameOfFunction } from "../../utils/namingUtils";
import { calculatePageNumber } from "../../utils/paginationUtils";
import { parseQueryStringToObject } from "../../utils/requestParameterUtils";
import { getRoute } from "../../utils/routeUtils";
import { PaginationControl } from "../common/controls/pagination/paginationControl";
import Sorting from "../common/controls/sorting/sorting";
import { FilterData, ListRequestHandler } from "../common/lists/listRequestHandler";
import { NoData } from "../common/presentation/noData";
import { Constants } from "../constants";
import { routeLinks } from "../layout/routes/routeLinks";
import { Local } from "../localization/local";
import { localizer } from "../localization/localizer";
import { onAddToFavorites, onAddToCart, onDeleteProduct } from "./catalogActions";
import { ProductCard } from "./product/productCard";
import { AccessoriesFilterData } from "../../dataModels/catalog/accessoriesFilterData";
import { AccessoryType } from "../../dataModels/enums/accessory/accessoryType";
import { AccessoriesFilter } from "./filters/accessoriesFilter";

const defaultFilter: AccessoriesFilterData = {
    accessoryTypes: [
        AccessoryType.Autoguidance,
        AccessoryType.BagsAndCases,
        AccessoryType.BarlowLens,
        AccessoryType.ForMountings,
        AccessoryType.LightFilter,
        AccessoryType.MountingsAndTripods,
        AccessoryType.Ocular,
        AccessoryType.SolarFilter,
    ],
    priceMin: 0,
    priceMax: 999999,
};

const defaultSorting: Sortable = {
    sortBy: "CreatedAt",
    direction: SortOrder.Descending,
};

const defaultPaging: Pageable = {
    pageNumber: 1,
    pageSize: Constants.paging.defaultPageSize,
};

const nameOfBinocularsFilterDataProperty = getNameOfFunction<AccessoriesFilterData>();

const getFilterDataFromQuery = (): AccessoriesFilterData => {
    const params = parseQueryStringToObject(window.location.search);
    const priceMin = params[nameOfBinocularsFilterDataProperty("priceMin")];
    const priceMax = params[nameOfBinocularsFilterDataProperty("priceMax")];

    if (window.location.search) {
        return {
            priceMin: priceMin,
            priceMax: priceMax,
        } as AccessoriesFilterData;
    }
};

const getInitialFilter = () => getFilterDataFromQuery() || defaultFilter;

export const CatalogAccessoriesPage = () => {
    const [products, setProducts] = useState<ListResult<ProductListItem>>();

    async function loadProducts(request: FilterData<{}>) {
        setProducts(await searchAccessories(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<AccessoriesFilterData>(
            getInitialFilter(), defaultPaging, defaultSorting, loadProducts), []);

    useAsyncEffect(async () => {
        await loadProducts(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(products?.totalCount);

    return (
        <div className="catalog-page">
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="Accessories" /></h1>
                </Col>
                {isStaff() &&
                    <Col>
                        <Button
                            onClick={() => sharedHistory.push(getRoute(routeLinks.catalog.createProduct))}
                            className="float-right">
                            <Local id="CreateProduct" />
                        </Button>
                    </Col>}
            </Row>
            <p className="catalog category-description">
                {localizer.get("AccessoriesCategoryDescription")}
            </p>
            <AccessoriesFilter onApply={listHandler.applyFilter}
                defaultFilter={defaultFilter} filterFromQuery={listHandler.getRequest()} />
            <div>
                <div className="d-flex align-items-center">
                    <div className="mr-auto"><Local id="Found" />: <b>{products?.totalCount}</b></div>
                    <Sorting
                        default={defaultSorting}
                        sortKeys={["CreatedAt", "Price"]}
                        onChange={listHandler.applyListOptions} />
                </div>
                <Row className="p-3">
                    {products?.totalCount > 0
                        ? products.items.map((product, ind) =>
                            <ProductCard
                                className="col-2"
                                key={ind}
                                product={product}
                                onAddToFavorites={async () => await onAddToFavorites(product.productId)}
                                onAddToCart={async () => await onAddToCart(product.productId)}
                                onEditProduct={() => sharedHistory.push(getRoute(routeLinks.catalog.editProduct, product.productId))}
                                onDeleteProduct={() => onDeleteProduct(product.productId)} />)
                        : <NoData />}
                </Row>
            </div>
            {pagesCount > 1 &&
                <div className="mt-3 justify-content-center">
                    <PaginationControl
                        totalPages={pagesCount}
                        onChange={x => listHandler.applyListOptions({ pageNumber: x })}
                        extPageHandler={listHandler.pageHandler} />
                </div>}
        </div>
    );
};