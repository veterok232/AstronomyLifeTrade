import React from "react";
import MenuToggle from "../menuToggle/menuToggle";
import { CatalogSearchBar } from "../catalogSearch/catalogSearchBar";
import { HeaderActionButtons } from "./headerActionButtons";
import { onSearch, onCartOpen, onAstronomicalCalculatorOpen, onFavoritesOpen, onUserProfileOpen } from "../../actions/generalActions";

export const Header = () => {
    return (
        <header className="grid__header header">
            <MenuToggle className="header__menu-toggle" />
            <CatalogSearchBar onSearch={onSearch}/>
            <HeaderActionButtons
                onCartOpen={onCartOpen}
                onAstronomicalCalculatorOpen={onAstronomicalCalculatorOpen}
                onFavoritesOpen={onFavoritesOpen}
                onUserProfileOpen={onUserProfileOpen}/>
        </header>
    );
};

export default Header;