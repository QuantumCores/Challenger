import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateHelper {

    constructor() { }

    getDateOnly(dateTime: Date): Date {
        let year = dateTime.getFullYear();
        let month = dateTime.getMonth();
        let day = dateTime.getDate();

        return new Date(year, month, day);
    }

    getDateOnlyAsNumber(dateTime: Date): number {
        let year = dateTime.getFullYear();
        let month = dateTime.getMonth();
        let day = dateTime.getDate();

        return year * 10000 + (month + 1) * 100 + day;
    }
}