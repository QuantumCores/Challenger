import { FitRecordDto } from "./components/fit-record-item/fitRecordDto";


export const FITRECORDS: FitRecordDto[] = [
    {
        id: 1,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Bieganie',
        repetitions: 8,
        duration: 12,
        distance: 12,
    },
    {
        id: 3,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Snowboard',
        repetitions: 6,
        duration: 11,
        distance: 3,
    },
    {
        id: 4,
        userId: 23,
        recordDate: new Date(2022, 1, 23),
        excersize: 'Pływanie',
        repetitions: 4,
        duration: 123,
        distance: 7,
    },
]