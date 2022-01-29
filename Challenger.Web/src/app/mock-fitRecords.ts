import { FitRecordDto } from "./components/fit-record-item/fitRecordDto";


export const FITRECORDS: FitRecordDto[] = [
    {
        Id: 1,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Bieganie',
        Repetitions: 8,
        Duration: 12,
        Distance: 12,
    },
    {
        Id: 3,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Snowboard',
        Repetitions: 6,
        Duration: 11,
        Distance: 3,
    },
    {
        Id: 4,
        UserId: 23,
        RecordDate: new Date(2022, 1, 23),
        Excersize: 'Pływanie',
        Repetitions: 4,
        Duration: 123,
        Distance: 7,
    },
]