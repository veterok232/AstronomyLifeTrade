import { getCurrentCstDate } from "../../api/date/dateApi";
import { getTomorrowStartDate } from "../../utils/dateTimeUtils";

class DatesStore {
    private currentCstDateWillBeExpiredAt?: Date;
    private currentCstDate?: Date;

    public async getCurrentCstDate(): Promise<Date> {
        if (this.currentCstDate == undefined || this.isCurrentCstDateOutdated()) {
            this.currentCstDateWillBeExpiredAt = getTomorrowStartDate();
            this.currentCstDate = await getCurrentCstDate();
        }

        return this.currentCstDate;
    }

    public async getCurrentYear(): Promise<number> {
        return (await this.getCurrentCstDate()).getFullYear();
    }

    private isCurrentCstDateOutdated(): boolean {
        return new Date().getTime() > this.currentCstDateWillBeExpiredAt.getTime();
    }
}

export const datesStore = new DatesStore();