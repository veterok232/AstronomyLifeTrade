import React, { useState } from "react";
import Rating from "@mui/material/Rating";
import { ProductRating } from "../../../dataModels/catalog/product/productRating";
import useAsyncEffect from "use-async-effect";
import { getProductRating } from "../../../api/catalog/catalogApi";

interface Props {
    productId: string;
    productRating?: ProductRating;
}

export const ProductRatingSection = (props: Props) => {
    const [productRating, setProductRating] = useState<ProductRating>();

    useAsyncEffect(async () => {
        setProductRating(props.productRating
            ? props.productRating
            : await getProductRating(props.productId));
    }, []);

    return (
        <div>
            <Rating name="read-only" value={productRating.rating} readOnly />
            <span>({productRating.commentsCount})</span>
        </div>

    );
};