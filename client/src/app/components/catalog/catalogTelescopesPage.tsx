import React, { useMemo, useState } from "react";
import { localizer } from "../localization/localizer";
import { Col, Row } from "reactstrap";
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

const defaultFilter: TelescopesFilterData = {
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

const onAddToCart = () => {};

const onAddToFavorites = () => {};

export const CatalogTelescopesPage = () => {
    const [products, setProducts] = useState<ListResult<ProductListItem>>();

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

    return (
        <div className="catalog-page">
            <Row>
                <Col xs={4} >
                     <h1 className="ui-page-header pt-2"><Local id="Telescopes" /></h1>
                </Col>
            </Row>
            <p className="catalog category-description">
                {localizer.get("TelescopesCategoryDescription")}
            </p>
            <TelescopesFilter onApply={listHandler.applyFilter}
                defaultFilter={defaultFilter} filterFromQuery={listHandler.getRequest()} />
            <div className="contracts-font">
                <div className="d-flex align-items-center">
                    <div className="mr-auto"><Local id="Found" />: <b>{products?.totalCount}</b></div>
                    <Sorting
                        default={defaultSorting}
                        sortKeys={["CreatedAt", "Price"]}
                        onChange={listHandler.applyListOptions} />
                </div>
                {products?.totalCount > 0
                    ? products.items.map((product, ind) =>
                        <ProductCard key={ind}
                            product={product}
                            onAddToFavorites={onAddToFavorites}
                            onAddToCart={onAddToCart} />)
                    : <NoData />
                }
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