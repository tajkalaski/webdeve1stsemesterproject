import { Question } from './question';

export interface QuestionSubCategory {
    id: string,
    name: string,
    description: string,
    questions: Question[],
    expanded: boolean,
} 