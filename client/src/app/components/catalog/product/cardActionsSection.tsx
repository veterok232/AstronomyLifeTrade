import React from "react";
import { AddToCartIconicButton } from "../../common/controls/buttons/productCard/addToCartIconicButton";
import { AddToFavoritesIconicButton } from "../../common/controls/buttons/productCard/addToFavoritesIconicButton";
import { isStaff } from "../../../infrastructure/services/auth/authService";
import { DeleteProductIconicButton } from "../../common/controls/buttons/productCard/deleteProductIconicButton";
import { EditProductIconicButton } from "../../common/controls/buttons/productCard/editProductIconicButton";

interface Props {
    onAddToFavorites: () => void;
    onAddToCart: () => void;
    onDeleteProduct: () => void;
    onEditProduct: () => void;
}

export const CardActionsSection = (props: Props) => {
    return (
        <div className="d-flex flex-row">
            {!isStaff() && <>
                <AddToCartIconicButton onClick={props.onAddToCart} />
                <AddToFavoritesIconicButton onClick={props.onAddToCart} />
            </>}
            {isStaff() && <>
                <EditProductIconicButton onClick={props.onEditProduct} />
                <DeleteProductIconicButton onClick={props.onDeleteProduct} />
            </>}
        </div>
    );
};