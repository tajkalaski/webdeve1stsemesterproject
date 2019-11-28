
export interface CompanyCertificate {
    id: string;
    name: string;
    overallRating: string;
    certifiedFrom: Date;
    certifiedTo: Date;
    companyCertificateSubRatings: any[];
}
