import { GymRecordDto } from "./components/gym-record-item/gymRecordDto";

export const GYMRECORDS: GymRecordDto[] = [
    {
        Id: 1,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Ławka płasko',
        Weight: 80,
        Repetitions: 8,
        MuscleGroup: 'Chest'
    },
    {
        Id: 3,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Ławka płasko',
        Weight: 85,
        Repetitions: 6,
        MuscleGroup: 'Chest'
    },
    {
        Id: 4,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Ławka płasko',
        Weight: 90,
        Repetitions: 4,
        MuscleGroup: 'Chest'
    },
]