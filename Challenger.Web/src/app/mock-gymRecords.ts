import { GymRecordDto } from "./components/gym-record-item/gymRecordDto";

export const GYMRECORDS: GymRecordDto[] = [
    {
        id: 1,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Ławka płasko',
        weight: 80,
        repetitions: 8,
        muscleGroup: 'Chest'
    },
    {
        id: 3,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Ławka płasko',
        weight: 85,
        repetitions: 6,
        muscleGroup: 'Chest'
    },
    {
        id: 4,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Ławka płasko',
        weight: 90,
        repetitions: 4,
        muscleGroup: 'Chest'
    },
]