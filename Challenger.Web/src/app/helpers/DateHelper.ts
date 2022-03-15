import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateHelper {

    constructor() { }

    milisecondsInDay: number = 86400000;
    milisecondsInWeek: number = 604800000;

    getWeek(date: Date): number {
        const start = new Date(date.getFullYear(), 0, 1);
        return Math.ceil(((date.getTime() - start.getTime()) / this.milisecondsInDay + start.getDay() + 1) / 7) - 1;
    }

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

    addDays(date: Date, days:number): Date {
        return new Date(date.getTime() + days * this.milisecondsInDay);
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