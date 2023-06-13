/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/require-await */
import React, { useMemo, useState } from "react";
import { localizer } from "../localization/localizer";
import { Button, Col, Form, Input, Row } from "reactstrap";
import { Local } from "../localization/local";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import useAsyncEffect from "use-async-effect";
import { NoData } from "../common/presentation/noData";
import { ProductCard } from "./product/productCard";
import { TelescopesFilterData } from "../../dataModels/catalog/telescopesFilterData";
import { Sortable } from "../../dataModels/common/sortable";
import { SortOrder } from "../../dataModels/enums/sortOrder";
import { Constants } from "../constants";
import { parseQueryStringToObject } from "../../utils/requestParameterUtils";
import { getNameOfFunction } from "../../utils/namingUtils";
import { ListResult } from "../../dataModels/common/listResult";
import { FilterData, ListRequestHandler } from "../common/lists/listRequestHandler";
import { calculatePageNumber } from "../../utils/paginationUtils";
import { PaginationControl } from "../common/controls/pagination/paginationControl";
import Sorting from "../common/controls/sorting/sorting";
import { Pageable } from "../../dataModels/common/pageable";
import { TelescopesFilter } from "./filters/telescopesFilter";
import { searchTelescopes } from "../../api/catalog/catalogApi";
import { onAddToCart, onAddToFavorites, onDeleteProduct } from "./catalogActions";
import { TelescopeType } from "../../dataModels/enums/telescope/telescopeType";
import { generateRandomString } from "../../utils/stringUtils";
import { uploadFile } from "../../api/file/filesApi";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import { isStaff } from "../../infrastructure/services/auth/authService";

const defaultFilter: TelescopesFilterData = {
    telescopeTypes: [
        TelescopeType.Refractor,
        TelescopeType.Reflector,
        TelescopeType.MirrorLens,
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

const nameOfTelescopesFilterDataProperty = getNameOfFunction<TelescopesFilterData>();

const getFilterDataFromQuery = (): TelescopesFilterData => {
    const params = parseQueryStringToObject(window.location.search);
    const priceMin = params[nameOfTelescopesFilterDataProperty("priceMin")];
    const priceMax = params[nameOfTelescopesFilterDataProperty("priceMax")];
    const userLevels = params[nameOfTelescopesFilterDataProperty("userLevels")];

    if (window.location.search) {
        return {
            priceMin: priceMin,
            priceMax: priceMax,
            userLevels: userLevels,
        } as TelescopesFilterData;
    }
};

const getInitialFilter = () => getFilterDataFromQuery() || defaultFilter;

export const CatalogTelescopesPage = () => {
    const [products, setProducts] = useState<ListResult<ProductListItem>>();
    const [fileInputKey, setFileInputKey] = useState(generateRandomString());

    async function loadProducts(request: FilterData<{}>) {
        setProducts(await searchTelescopes(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<TelescopesFilterData>(
            getInitialFilter(), defaultPaging, defaultSorting, loadProducts), []);

    useAsyncEffect(async () => {
        await loadProducts(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(products?.totalCount);

    const uploadFiles = async (fileInputChangeEvent: React.ChangeEvent<HTMLInputElement>) => {
        for (const file of Array.from(fileInputChangeEvent.target.files)) {
            await uploadFile(file);
        }

        setFileInputKey(generateRandomString());
    };

    return (
        <div className="catalog-page">
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="Telescopes" /></h1>
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
                {localizer.get("TelescopesCategoryDescription")}
            </p>
            <TelescopesFilter onApply={listHandler.applyFilter}
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