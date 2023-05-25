export function filterValues<TValue>(sourceDataMap: Array<[boolean, TValue]>): TValue[] {
    const resultArray: Array<TValue> = [];

    sourceDataMap.forEach((item) => {
        if (item[0]) {
            resultArray.push(item[1]);
        }
    });

    return resultArray;
}