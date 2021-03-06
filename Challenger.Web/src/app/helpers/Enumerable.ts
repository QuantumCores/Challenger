import { Injectable } from "@angular/core";
import { Grouping } from "./Grouping";

@Injectable({
    providedIn: 'root'
})
export class Enumerable {

    static groupBy<T>(array: T[], func: (item: T) => string): Grouping<T> {
        let result = new Grouping<T>();
        const asAny = result as any;
        array.forEach(x => {
            const key = func(x);
            if (!asAny[key]) {
                asAny[key] = [];
            }
            asAny[key].push(x);
        });
        return result;
    };

    static distinct<T>(array: T[]): T[]{
        return array.filter((x, i, s) => s.indexOf(x) === i);
    }
}


