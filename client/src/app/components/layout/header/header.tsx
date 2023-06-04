import React from "react";
import MenuToggle from "../menuToggle/menuToggle";
import { CatalogSearchBar } from "../catalogSearch/catalogSearchBar";
import { HeaderActionButtons } from "./headerActionButtons";
import { onSearch, onCartOpen, onAstronomicalCalculatorOpen, onFavoritesOpen, onUserProfileOpen, onOrdersOpen, onManagerProfileOpen } from "../../actions/generalActions";
import { Link } from "react-router-dom";
import { getDefaultPageRoute } from "../../../utils/routeUtils";
import { Greetings } from "../greetings";
import { contextStore } from "../../../infrastructure/stores/contextStore";

export const Header = () => {
    return (
        <header className="grid__header header">
            <MenuToggle className="header__menu-toggle" />
            <Link className="header__logo mx-2" to={getDefaultPageRoute()}><img className="header__logo" src="static/images/logo.png" /></Link>
            <CatalogSearchBar onSearch={onSearch}/>
            {contextStore.isAuthenticated &&
                <Greetings className="header__greetings"/>}
            <HeaderActionButtons
                className="header__action-buttons mr-2"
                onCartOpen={onCartOpen}
                onAstronomicalCalculatorOpen={onAstronomicalCalculatorOpen}
                onFavoritesOpen={onFavoritesOpen}
                onUserProfileOpen={onUserProfileOpen}
                onOrdersOpen={onOrdersOpen}
                onManagerProfileOpen={onManagerProfileOpen} />
        </header>
    );
};

export default Header;