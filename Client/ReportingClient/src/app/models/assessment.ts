import AssessmentQuestion from './assessment-question';

export default interface Assessment {
    id: string;
    title: string;
    companyId: string;
    assessmentQuestions: AssessmentQuestion[];
}
