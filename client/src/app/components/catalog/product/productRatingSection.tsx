import React, { useEffect, useState } from "react";
import Rating from "@mui/material/Rating";
import { ProductRating } from "../../../dataModels/catalog/product/productRating";

interface Props {
    className?: string;
    productRating?: ProductRating;
    size?: "small" | "medium" | "large";
}

const defaultRating: ProductRating = { rating: 0, commentsCount: 0 };

export const ProductRatingSection = (props: Props) => {
    const [productRating, setProductRating] = useState<ProductRating>(defaultRating);

    useEffect(() => {
        setProductRating(props.productRating ?? defaultRating);
    }, [props.productRating]);

    return (
        <div className={`d-flex align-items-center ${props.className}`}>
            <Rating size={`${props.size ? props.size : "small"}`} name="productRating" value={productRating?.rating} readOnly />
            <span className="align-self-center">({productRating?.commentsCount})</span>
        </div>

    );
};