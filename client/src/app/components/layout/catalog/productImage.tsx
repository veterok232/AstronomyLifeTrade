import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ImagePreview } from "../../common/presentation/imagePreview";
import { Constants } from "../../constants";
import { getFileAnonymousDownloadLink } from "../../../api/file/filesApi";
import { getLinkToProductDetails } from "../../../api/catalog/catalogApi";

interface Props {
    className?: string;
    productId: string;
    productImageId?: string;
}

const getProductImageUrl = (imageFileId?: string) => {
    if (!imageFileId) {
        return Constants.defaultProductImagePath;
    }

    return getFileAnonymousDownloadLink(imageFileId);
};

export const ProductImage = (props: Props) => {
    const [productImageUrl, setProductImageUrl] = useState<string>(null);

    useEffect(() => {
        setProductImageUrl(getProductImageUrl(props.productImageId));
    }, [props.productImageId]);

    return (
        <Link to={getLinkToProductDetails(props.productId)} >
            <ImagePreview
                className={`card-product-image ${props.className}`}
                image={productImageUrl} />
        </Link>
    );
};