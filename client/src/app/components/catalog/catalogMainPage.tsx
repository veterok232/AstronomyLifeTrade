import React from "react";
import { PopularProductsSection } from "./popularProductsSection/popularProductsSection";
import { localizer } from "../localization/localizer";
import { onAddToCart, onAddToFavorites } from "./catalogActions";

export const CatalogMainPage = () => {
    return (
        <div className="catalog-main-page">
            <p>
                {localizer.get("MainPageDescription")}
            </p>
            <PopularProductsSection
                onAddToCart={onAddToCart}
                onAddToFavorites={onAddToFavorites} />
        </div>
    );
};