import React from "react";
import { PopularProductsSection } from "./popularProductsSection/popularProductsSection";
import { localizer } from "../localization/localizer";

export const CatalogMainPage = () => {
    return (
        <div className="catalog-main-page">
            <p>
                {localizer.get("MainPageDescription")}
            </p>
            <PopularProductsSection />
        </div>
    );
};