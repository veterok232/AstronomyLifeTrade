import React from "react";
import { AddToCartIconicButton } from "../../common/controls/buttons/productCard/addToCartIconicButton";
import { AddToFavoritesIconicButton } from "../../common/controls/buttons/productCard/addToFavoritesIconicButton";

interface Props {
    onAddToFavorites: () => void;
    onAddToCart: () => void;
}

export const CardActionsSection = (props: Props) => {
    return (
        <div className="d-flex flex-row">
            <AddToCartIconicButton onClick={props.onAddToCart} />
            <AddToFavoritesIconicButton onClick={props.onAddToCart} />
        </div>
    );
};