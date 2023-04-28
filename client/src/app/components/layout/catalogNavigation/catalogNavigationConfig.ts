import { NavigationItemType } from "../../../dataModels/enums/navigationItemType";
import { MenuItem } from "../../../dataModels/menu/menuItem";
import { MenuItemConfig } from "../../../dataModels/menu/menuItemConfig";
import { routeLinks } from "../routes/routeLinks";

export const getMenuItems = (): Array<MenuItem> => {
    return getUserAccessibleMenuItems(menuItemsConfig);
};

const getUserAccessibleMenuItems = (items: Array<MenuItemConfig>): Array<MenuItem> => {
    const result = new Array<MenuItem>();

    for (const item of items.filter(item => isUserHasAccessToItem(item))) {
        result.push(item.type === NavigationItemType.Link ? { ...item } : createContainerItem(item));
    }

    return result;
};

const createContainerItem = (item: MenuItemConfig) => {
    const childItems = getUserAccessibleMenuItems(item.childItems);

    const newItem = {
        ...item,
        childItems,
    };

    if (childItems.length === 0) {
        newItem.type = NavigationItemType.Link;
        newItem.to = routeLinks.errors.forbidden;
    }

    return newItem;
};

const isUserHasAccessToItem = (item: MenuItemConfig) => {
    return (item.availableForRoles === undefined || isAuthorizedAsOneOf(item.availableForRoles)) &&
        (item.availableForPermissions === undefined || hasAllOfPermissions(item.availableForPermissions));
};

const menuItemsConfig: Array<MenuItemConfig> = [{
    type: NavigationItemType.Link,
    iconName: "dashboard",
    titleKey: "Dashboard",
    availableForPermissions: [Permissions.dashboard.section],
    to: routeLinks.dashboards.root,
    rootFor: [routeLinks.dashboards.root],
}, {
    type: NavigationItemType.Link,
    iconName: "dashboard",
    titleKey: "Dashboard",
    availableForRoles: [Roles.consumer],
    to: routeLinks.consumer.dashboard,
    rootFor: [
        routeLinks.dashboards.root,
        routeLinks.contracts.root,
    ],
}, {
    type: NavigationItemType.Link,
    iconName: "library_books",
    titleKey: "Contracts",
    availableForPermissions: [Permissions.contracts.section],
    to: routeLinks.contracts.root,
    rootFor: [routeLinks.contracts.root],
}, {
    type: NavigationItemType.Link,
    iconName: "payments",
    titleKey: "ReserveRequests",
    availableForPermissions: [Permissions.reserveRequests.section],
    to: routeLinks.reserveRequests.root,
    rootFor: [routeLinks.reserveRequests.root],
}, {
    type: NavigationItemType.Container,
    iconName: "file_copy",
    titleKey: "Documents",
    availableForPermissions: [Permissions.documents.section],
    rootFor: [routeLinks.documents.root],
    childItems: [{
        type: NavigationItemType.Link,
        titleKey: "DocumentCategory.HelpAndSupport",
        availableForPermissions: [Permissions.documents.section],
        to: routeLinks.documents.helpAndSupport,
        rootFor: [routeLinks.documents.helpAndSupport],
    }, {
        type: NavigationItemType.Link,
        titleKey: "DocumentCategory.StateLicenseRequirements",
        availableForPermissions: [Permissions.documents.section],
        to: routeLinks.documents.stateLicenseRequirements,
        rootFor: [routeLinks.documents.stateLicenseRequirements],
    }, {
        type: NavigationItemType.Link,
        titleKey: "DocumentCategory.MasterDealerAgreements",
        availableForPermissions: [Permissions.documents.section],
        to: routeLinks.documents.masterDealerAgreements,
        rootFor: [routeLinks.documents.masterDealerAgreements],
    }, {
        type: NavigationItemType.Link,
        titleKey: "DocumentCategory.OnlineOrderRequests",
        availableForPermissions: [Permissions.documents.section],
        to: routeLinks.documents.onlineOrderRequests,
        rootFor: [routeLinks.documents.onlineOrderRequests],
    }, {
        type: NavigationItemType.Link,
        titleKey: "DocumentCategory.TermsAndPromotions",
        availableForPermissions: [Permissions.documents.section],
        to: routeLinks.documents.termsAndPromotions,
        rootFor: [routeLinks.documents.termsAndPromotions],
    }, {
        type: NavigationItemType.Container,
        titleKey: "Forms",
        availableForPermissions: [Permissions.documents.section],
        rootFor: [routeLinks.documents.forms.root],
        childItems: [{
            type: NavigationItemType.Link,
            titleKey: "DocumentCategory.CreditApplicationForms",
            availableForPermissions: [Permissions.documents.section],
            to: routeLinks.documents.forms.creditApplication,
            rootFor: [routeLinks.documents.forms.creditApplication],
        }, {
            type: NavigationItemType.Link,
            titleKey: "DocumentCategory.RecurringAchCcForms",
            availableForPermissions: [Permissions.documents.section],
            to: routeLinks.documents.forms.recurring,
            rootFor: [routeLinks.documents.forms.recurring],
        }],
    }, {
        type: NavigationItemType.Link,
        iconName: "delete",
        titleKey: "DocumentCategory.Deleted",
        availableForRoles: [Roles.staff],
        availableForPermissions: [Permissions.documents.manageDocuments],
        to: routeLinks.documents.deleted,
        rootFor: [routeLinks.documents.deleted],
        isSeparated: true,
    }],
}, {
    type: NavigationItemType.Link,
    iconName: "supervisor_account",
    titleKey: "Users",
    availableForPermissions: [Permissions.users.section],
    to: routeLinks.users.root,
    rootFor: [routeLinks.users.root],
}, {
    type: NavigationItemType.Link,
    iconName: "business_center",
    titleKey: "Distributors",
    availableForPermissions: [Permissions.distributors.section],
    to: routeLinks.distributors.root,
    rootFor: [routeLinks.distributors.root],
}, {
    type: NavigationItemType.Link,
    iconName: "description",
    titleKey: "News",
    availableForRoles: [],
    to: routeLinks.news,
    rootFor: [routeLinks.news],
}, {
    type: NavigationItemType.Link,
    iconName: "today",
    titleKey: "AuditLog",
    availableForRoles: [Roles.staff],
    to: routeLinks.auditLog,
    rootFor: [routeLinks.auditLog],
}, {
    type: NavigationItemType.Container,
    iconName: "monetization_on",
    titleKey: "Payments",
    availableForRoles: [Roles.staff, Roles.consumer],
    availableForPermissions: [Permissions.payments.section],
    rootFor: [routeLinks.payments.root],
    childItems: [{
        type: NavigationItemType.Link,
        titleKey: "Payments_OneTime",
        availableForRoles: [Roles.staff, Roles.consumer],
        availableForPermissions: [Permissions.payments.oneTime],
        to: routeLinks.payments.oneTime,
        rootFor: [routeLinks.payments.oneTime],
    }, {
        type: NavigationItemType.Link,
        titleKey: "Payments_Recurring",
        availableForRoles: [Roles.staff, Roles.consumer],
        availableForPermissions: [Permissions.payments.recurring.section],
        to: routeLinks.payments.recurring.root,
        rootFor: [routeLinks.payments.recurring.root],
    }],
}, {
    type: NavigationItemType.Container,
    iconName: "settings",
    titleKey: "Settings",
    availableForPermissions: [Permissions.settings.section],
    rootFor: [routeLinks.settings.root],
    childItems: [{
        type: NavigationItemType.Link,
        titleKey: "States",
        availableForPermissions: [Permissions.settings.states.section],
        to: routeLinks.settings.states,
        rootFor: [routeLinks.settings.states],
    }, {
        type: NavigationItemType.Link,
        titleKey: "Promotions",
        availableForPermissions: [Permissions.settings.promotions.section],
        to: routeLinks.settings.promotions.root,
        rootFor: [routeLinks.settings.promotions.root],
    }, {
        type: NavigationItemType.Link,
        titleKey: "ProductsAndSubProducts",
        availableForPermissions: [Permissions.settings.productsAndSubproducts.section],
        to: routeLinks.settings.products,
        rootFor: [routeLinks.settings.products],
    }, {
        type: NavigationItemType.Link,
        titleKey: "Terms",
        availableForPermissions: [Permissions.settings.terms.section],
        to: routeLinks.settings.terms,
        rootFor: [routeLinks.settings.terms],
    }, {
        type: NavigationItemType.Link,
        titleKey: "DaysToFirstPayment",
        availableForPermissions: [Permissions.settings.daysToFirstPayment.section],
        to: routeLinks.settings.daysToFirstPayment,
        rootFor: [routeLinks.settings.daysToFirstPayment],
    },
    {
        type: NavigationItemType.Container,
        titleKey: "BuyRate",
        availableForPermissions: [Permissions.settings.buyRate.section],
        rootFor: [routeLinks.settings.buyRate.root],
        childItems: [{
            type: NavigationItemType.Link,
            titleKey: "Fico",
            availableForPermissions: [Permissions.settings.buyRate.fico.section],
            to: routeLinks.settings.buyRate.fico,
            rootFor: [routeLinks.settings.buyRate.fico],
        }, {
            type: NavigationItemType.Link,
            titleKey: "Trades",
            availableForPermissions: [Permissions.settings.buyRate.trades.section],
            to: routeLinks.settings.buyRate.trades,
            rootFor: [routeLinks.settings.buyRate.trades],
        }, {
            type: NavigationItemType.Link,
            titleKey: "Incomes",
            availableForPermissions: [Permissions.settings.buyRate.incomes.section],
            to: routeLinks.settings.buyRate.incomes,
            rootFor: [routeLinks.settings.buyRate.incomes],
        }, {
            type: NavigationItemType.Link,
            titleKey: "CoApplicantsLogic",
            availableForPermissions: [Permissions.settings.buyRate.coApplicantLogic.section],
            to: routeLinks.settings.buyRate.coApplicantLogic,
            rootFor: [routeLinks.settings.buyRate.coApplicantLogic],
        }, {
            type: NavigationItemType.Link,
            titleKey: "Residence",
            availableForPermissions: [Permissions.settings.buyRate.residence.section],
            to: routeLinks.settings.buyRate.residence,
            rootFor: [routeLinks.settings.buyRate.residence],
        }, {
            type: NavigationItemType.Link,
            titleKey: "BuyRateAdjustment",
            availableForPermissions: [Permissions.settings.buyRate.adjustmentByStateAndProduct.section],
            to: routeLinks.settings.buyRate.adjustment,
            rootFor: [routeLinks.settings.buyRate.adjustment],
        }, {
            type: NavigationItemType.Link,
            titleKey: "BuyRateProgramB",
            availableForPermissions: [Permissions.settings.buyRate.programB.section],
            to: routeLinks.settings.buyRate.programB,
            rootFor: [routeLinks.settings.buyRate.programB],
        }]
    }, {
        type: NavigationItemType.Link,
        titleKey: "Paperwork",
        availableForPermissions: [Permissions.settings.paperwork.section],
        to: routeLinks.settings.paperwork,
        rootFor: [routeLinks.settings.paperwork],
    }, {
        type: NavigationItemType.Container,
        titleKey: "Templates",
        availableForPermissions: [Permissions.settings.templates.section],
        rootFor: [routeLinks.settings.templates.root],
        childItems: [{
            type: NavigationItemType.Link,
            titleKey: "TemplatesManagement",
            availableForPermissions: [Permissions.settings.templates.section],
            to: routeLinks.settings.templates.management,
            rootFor: [routeLinks.settings.templates.management],
        }, {
            type: NavigationItemType.Link,
            titleKey: "Tags",
            availableForPermissions: [Permissions.settings.templates.section],
            to: routeLinks.settings.templates.tags.root,
            rootFor: [routeLinks.settings.templates.tags.root],
        }]
    }],
}, {
    type: NavigationItemType.Container,
    iconName: "build",
    titleKey: "Configuration.Section",
    availableForRoles: [Roles.staff],
    availableForPermissions: [Permissions.configuration.section],
    rootFor: [routeLinks.configuration.root],
    childItems: [
        {
            type: NavigationItemType.Link,
            titleKey: "Configuration.General",
            availableForPermissions: [Permissions.configuration.general],
            to: routeLinks.configuration.general,
            rootFor: [routeLinks.configuration.general],
        }, {
            type: NavigationItemType.Link,
            titleKey: "Configuration.ThirdParty",
            availableForPermissions: [Permissions.configuration.thirdParty],
            to: routeLinks.configuration.thirdParty,
            rootFor: [routeLinks.configuration.thirdParty],
        },
    ]
}];