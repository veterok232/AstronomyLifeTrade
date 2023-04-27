const dateResource = "dates";

export async function getCurrentCstDate(): Promise<Date> {
    const date = await httpGet<Date>({
        url: `${dateResource}/current-cst-date`,
        silent: true,
    });

    return handleDateResponse(date);
}