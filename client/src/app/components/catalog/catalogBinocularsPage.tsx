import React, { useState, useMemo } from "react";
import { Row, Col, Button } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { searchBinoculars } from "../../api/catalog/catalogApi";
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
import { BinocularFilterData } from "../../dataModels/catalog/binocularFilterData";
import { BinocularPurpose } from "../../dataModels/enums/binocuclar/binocularPurpose";
import { BinocularsFilter } from "./filters/binocularsFilter";

const defaultFilter: BinocularFilterData = {
    binocularPurposes: [
        BinocularPurpose.Army,
        BinocularPurpose.Astronomical,
        BinocularPurpose.Children,
        BinocularPurpose.HuntingAndFishing,
        BinocularPurpose.Navy,
        BinocularPurpose.Sport
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

const nameOfBinocularsFilterDataProperty = getNameOfFunction<BinocularFilterData>();

const getFilterDataFromQuery = (): BinocularFilterData => {
    const params = parseQueryStringToObject(window.location.search);
    const priceMin = params[nameOfBinocularsFilterDataProperty("priceMin")];
    const priceMax = params[nameOfBinocularsFilterDataProperty("priceMax")];

    if (window.location.search) {
        return {
            priceMin: priceMin,
            priceMax: priceMax,
        } as BinocularFilterData;
    }
};

const getInitialFilter = () => getFilterDataFromQuery() || defaultFilter;

export const CatalogBinocularsPage = () => {
    const [products, setProducts] = useState<ListResult<ProductListItem>>();

    async function loadProducts(request: FilterData<{}>) {
        setProducts(await searchBinoculars(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<BinocularFilterData>(
            getInitialFilter(), defaultPaging, defaultSorting, loadProducts), []);

    useAsyncEffect(async () => {
        await loadProducts(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(products?.totalCount);

    return (
        <div className="catalog-page">
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="Binoculars" /></h1>
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
                {localizer.get("BinocularsCategoryDescription")}
            </p>
            <BinocularsFilter onApply={listHandler.applyFilter}
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
                                onDeleteProduct={async () => await onDeleteProduct(product.productId)} />)
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