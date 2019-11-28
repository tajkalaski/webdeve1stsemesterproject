import { QuestionSubCategory } from './question-sub-category';

export interface QuestionCategory {
    id: string,
    name: string,
    description: string,
    questionSubCategories: QuestionSubCategory[],
    expanded: boolean,
} 