import { FileExtensions } from "./fileExtensions";

export const Constants = {
    autoLogoutTimerInSec: 60,
    validDigitsLimitInInt: 9,
    defaultDecimalDigitsLimit: 2,
    uploadFilesSizeLimitInBytes: 52428800, // 50MB
    uploadImageSizeLimitInBytes: 5242880, // 5MB
    uploadCertificateSizeLimitInBytes: 5242880, // 5MB
    uploadLicenseSizeLimitInBytes: 5242880, // 5MB
    daysInYear: 365,
    daysInMonth: 30,
    monthsInYear: 12,
    hundredYearsInMonths: 1200,
    secondsInMinute: 60,
    millisecondsInSecond: 1000,
    moneyPrecision: 2,
    defaultDecimalPrecision: 2,
    percentsMaxValue: 100,
    minAge: 18,
    maxAge: 115,
    fieldMaxLengths: {
        emailMaxLength: 50,
        shortText: 25,
        defaultText: 255,
        tagStaticText: 10000,
    },
    specificFieldLengths: {
        phone: 10,
    },
    paging: {
        defaultPageSize: 10,
        maxPagingControlItemsCount: 5,
        itemsBeforeEllipsisCount: 3,
        defaultPageSizeOptions: [
            { value: 5 },
            { value: 10 },
            { value: 20 },
            { value: 50 },
            { value: 100 },
        ],
    },
    password: {
        minLength: 8,
        maxLength: 12,
        specialCharactersExpression: /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).+$/
    },
    defaultUserAvatarImagePath: "static/images/user-avatar.svg",
    defaultDistributorLogoImagePath: "static/images/default-logo.svg",
    defaultEmptyValue: "-",
    longDash: "—",
    dash: "-",
    rowNumber: "#",
    availableImageFileExtensions: [FileExtensions.jpg, FileExtensions.jpeg, FileExtensions.png],
    availableDocumentFilesExtensions: [
        FileExtensions.pdf,
        FileExtensions.doc,
        FileExtensions.docx,
        FileExtensions.bmp,
        FileExtensions.dib,
        FileExtensions.jpeg,
        FileExtensions.jpg,
        FileExtensions.png,
        FileExtensions.tiff,
        FileExtensions.tif,
        FileExtensions.gif,
        FileExtensions.xls,
        FileExtensions.xlsx,
    ],
    localStorageKeys: {
        userId: "user-id",
    },
    crossWindowEvents: {
        sessionExtended: "sessionExtended",
        logOutCompleted: "logOutCompleted",
        logInCompleted: "logInCompleted",
    },
    defaultMaxYear: 9999,
    limitedStringMaxLength: 50,
    secretPlaceholder: "*************",
    separators: {
        manualPickUpButtonsSeparator: "/",
    },
    phonePlaceholder: "(___) ___-____",
    createCommentForm: {
        textLengthMaxValue: 2000,
    },
};