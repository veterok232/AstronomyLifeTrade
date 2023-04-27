export class FilterDataStore {
    private filtersData: Map<string, {}> = new Map();

    public saveFilterData<TFilterData>(data: TFilterData, locationKey: string) {
        this.filtersData.set(locationKey, data);
    }

    public getFilterData<TFilterData>(locationKey: string): TFilterData {
        return this.filtersData.get(locationKey) as TFilterData;
    }
}

export const filterDataStore = new FilterDataStore();