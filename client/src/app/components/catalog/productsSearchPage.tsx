/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useMemo } from "react";
import { Row, Col } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { ListResult } from "../../dataModels/common/listResult";
import { Pageable } from "../../dataModels/common/pageable";
import { Sortable } from "../../dataModels/common/sortable";
import { SortOrder } from "../../dataModels/enums/sortOrder";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { calculatePageNumber } from "../../utils/paginationUtils";
import { getRoute } from "../../utils/routeUtils";
import { PaginationControl } from "../common/controls/pagination/paginationControl";
import Sorting from "../common/controls/sorting/sorting";
import { FilterData, ListRequestHandler } from "../common/lists/listRequestHandler";
import { NoData } from "../common/presentation/noData";
import { Constants } from "../constants";
import { routeLinks } from "../layout/routes/routeLinks";
import { Local } from "../localization/local";
import { onAddToFavorites, onAddToCart, onDeleteProduct } from "./catalogActions";
import { ProductCard } from "./product/productCard";
import { ProductsFilterData } from "../../dataModels/catalog/productsFilterData";
import { searchProducts } from "../../api/catalog/catalogApi";

const defaultSorting: Sortable = {
    sortBy: "CreatedAt",
    direction: SortOrder.Descending,
};

const defaultPaging: Pageable = {
    pageNumber: 1,
    pageSize: Constants.paging.defaultPageSize,
};

export const ProductsSearchPage = () => {
    const [products, setProducts] = useState<ListResult<ProductListItem>>();
    const params = new URLSearchParams(window.location.search);

    const initialFilter: ProductsFilterData = {
        searchString: params.get("searchString"),
    };

    async function loadProducts(request: FilterData<{}>) {
        setProducts(await searchProducts(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<ProductsFilterData>(
            initialFilter, defaultPaging, defaultSorting, loadProducts), []);

    useAsyncEffect(async () => {
        await loadProducts(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(products?.totalCount);

    return (
        <div className="catalog-page">
            <Row className="mb-3">
                <Col >
                    <h1 className="ui-page-header pt-2"><span><Local id="SearchProducts" />: {params?.get("searchString")}</span></h1>
                </Col>
            </Row>
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