import { Injectable } from "@angular/core";
import { IGrouping } from "./IGrouping";

@Injectable({
    providedIn: 'root'
})
export class Grouping<T> implements IGrouping<T>  {

    get(index: string): T[]
    {
        return (this as any)[index];
    }

    private _keys: string[];

    keys(): string[] {

        if (!this._keys) {
            this._keys = Object.keys(this).reverse();
        }

        return this._keys;
    }
}