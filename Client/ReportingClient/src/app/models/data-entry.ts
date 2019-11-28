import { SubQuestion } from './sub-question';
import { Worksite } from './worksite';
export interface DataEntry {
    id: string;
    name: string;
    SubQuestion: SubQuestion;
    Worksite: Worksite;
    to: Date;
    from: Date;
    value: number;

}