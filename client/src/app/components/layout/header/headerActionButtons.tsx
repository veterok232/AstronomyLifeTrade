import React from "react";
import { IconicCartButton } from "../../common/controls/buttons/headerActionButtons/iconicCartButton";
import { IconicFavoritesButton } from "../../common/controls/buttons/headerActionButtons/iconicFavoritesButton";
import { IconicUserProfileButton } from "../../common/controls/buttons/headerActionButtons/iconicUserProfileButton";

interface Props {
    onCartOpen: () => void;
    onUserProfileOpen: () => void;
    onFavoritesOpen: () => void;
    onAstronomicalCalculatorOpen: () => void;
}

export const HeaderActionButtons = (props: Props) => {
    return (
        <div>
            <IconicFavoritesButton onFavoritesOpen={props.onFavoritesOpen}/>
            <IconicCartButton onCartOpen={props.onCartOpen}/>
            <IconicUserProfileButton onUserProfileOpen={props.onUserProfileOpen}/>
        </div>
    );
};