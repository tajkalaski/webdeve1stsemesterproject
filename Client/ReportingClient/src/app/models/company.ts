

export interface Company {
    id: string;
    name: string;
    vatin: string;
    address: string;
    postalCode: string;
    city: string;
    country: string;
    annualRevenue: Int16Array;
    employees: Int16Array;
    financialYearStart: string;
    financialYearEnd: string;
    legalFormId: string;
    typeOfOwnershipId: string;
}
