export interface IGrouping<T> {
    get(index: string): T[];
    keys(): string[];
}

