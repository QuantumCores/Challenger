import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateHelper {

    constructor() { }

    getWeekDay(dateTime: Date) {
        let day = dateTime.getDay();
        if (day == 0) {
            return 7;
        }
        return day;
    }

    getDateOnly(dateTime: Date): Date {
        let year = dateTime.getFullYear();
        let month = dateTime.getMonth();
        let day = dateTime.getDate();

        return new Date(Date.UTC(year, month, day));
    }

    getDateOnlyAsNumber(dateTime: Date): number {
        let year = dateTime.getFullYear();
        let month = dateTime.getMonth();
        let day = dateTime.getDate();

        return year * 10000 + (month + 1) * 100 + day;
    }

    sortByDate<T>(array: T[], aFunc: (item: T) => Date, bFunc: (item: T) => Date): void {
        array.sort((a: T, b: T) => {
            return this.compareDates(bFunc(b), aFunc(a));
        });
    }

    sortByDateAscending<T>(array: T[], aFunc: (item: T) => Date, bFunc: (item: T) => Date): void {
        array.sort((a: T, b: T) => {
            return this.compareDates(aFunc(a), bFunc(b));
        });
    }

    compareDates(a: Date, b: Date): number {
        return a.getTime() - b.getTime();
    }
}