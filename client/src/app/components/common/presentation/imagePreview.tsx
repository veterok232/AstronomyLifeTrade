import React from "react";

interface Props {
    image: string;
    className?: string;
}

export const ImagePreview = (props: Props) => {
    return (
        <div className={`image-preview position-relative flex-shrink-0 ${props.className}`}>
            <div className="overflow-hidden h-100 w-100">
                <img src={props.image || ""} className="h-100 w-100" />
            </div>
        </div>);
};