import { DishDto } from "../food-add-dish/DishDto";
import { MealProductDto } from "../meal-product-add/MealProductDto";

export class MealRecordDto {

    id: number;
    isNextDay: boolean;
    mealName: string;
    recordTime: number;
    diaryRecordId: number;
    mealProducts: MealProductDto[];
    dishes: DishDto[];
}