import { FastRecordDto } from "../fast-record-add/FastRecordDto";
import { MealDishDto } from "../meal-dish-add/MealDishDto";
import { MealProductDto } from "../meal-product-add/MealProductDto";

export class MealRecordDto {

    id: number;
    isNextDay: boolean;
    mealName: string;
    recordTime: number;
    diaryRecordId: number;
    mealProducts: MealProductDto[];
    mealDishes: MealDishDto[];
    fastRecords: FastRecordDto[];
}