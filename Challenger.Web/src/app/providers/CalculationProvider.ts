import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class CalculationProvider {

    constructor() {
    }

    calculateFatNavyFormula(isMale: boolean, waist: number, neck: number, height: number, hips: number) {

        if (isMale) {
            let val = 495.0 / (1.0324 - 0.19077 * Math.log10(waist - neck) + 0.15456 * Math.log10(height)) - 450;
            return Math.round(val * 10)/10;
        }
        else {
            let val = 495.0 / (1.29579 - 0.35004 * Math.log10(waist + hips - neck) + 0.22100 * Math.log10(height)) - 450;
            return Math.round(val * 10)/10;
        }
    }
}