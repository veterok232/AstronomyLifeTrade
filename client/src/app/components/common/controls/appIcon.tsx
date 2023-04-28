import React from "react";

interface AppIconProps {
    icon: string;
    className?: string;
    onClick?: (e: React.MouseEvent<HTMLElement, MouseEvent>) => void;
    title?: string;
    id?: string;
}

export const AppIcon: React.FC<AppIconProps> = (props: AppIconProps) => (
    <i id={props.id} className={`material-icons ${props.className || ""}`} onClick={props.onClick} title={props.title}>{props.icon}</i>
);