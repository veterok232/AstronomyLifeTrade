import { LabeledValue } from "../components/common/controls/labeledValue";
export function filterValues<TValue>(sourceDataMap: Array<[boolean, TValue]>): TValue[] {
    const resultArray: Array<TValue> = [];

    sourceDataMap.forEach((item) => {
        if (item[0]) {
            resultArray.push(item[1]);
        }
    });

    return resultArray;
}

export function dataToOptions<T>(
    data: Array<T>,
    valueSelector: (d: T) => any,
    textSelector: (d: T) => string,
): LabeledValue[] {
    return (
        data?.map((d) => {
            const option: any = { value: valueSelector(d) };
            // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
            option["label"] = textSelector(d);
            return option;
        }) ?? []
    );
}