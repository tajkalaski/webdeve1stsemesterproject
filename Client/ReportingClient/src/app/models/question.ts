import { SubQuestion } from './sub-question';

export interface Question {
    id: string;
    name: string;
    description: string;
    subQuestions: SubQuestion[];
    expanded: boolean;
}
