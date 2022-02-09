export class FitRecordDto {

    id: number;
    userId: number;
    recordDate: Date;
    excersize: string;
    duration?: number;
    durationUnit?: string;
    distance?: number;
    repetitions?: number;
    burntCalories: number;
}