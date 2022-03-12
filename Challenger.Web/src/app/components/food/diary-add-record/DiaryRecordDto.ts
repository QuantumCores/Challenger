import { MealRecordDto } from "../meal-add-record/MealRecordDto";

export class DiaryRecordDto {

    id: number;
    userId: number;
    diaryDate: Date;
    mealRecords: MealRecordDto[];
    energy: number;
    fats: number;
    proteins: number;
    carbohydrates: number;
}