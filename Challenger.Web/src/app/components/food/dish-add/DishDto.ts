import { IngridientDto } from "../ingridient-add/IngridientDto";

export class DishDto {

    id: number;
    userId: number;
    name: string;
    ingridients: IngridientDto[]
    isPublic: boolean;
    preparationTime: number;
    servings: number;
    size: number;
    energy: number;
    fats: number;
    proteins: number;
    carbohydrates: number;
}