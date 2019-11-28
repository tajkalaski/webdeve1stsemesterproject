import { Division } from './Division';

export interface Worksite {
    id: string;
    Division: Division;
    address: string;
    postalcode: string;
    city: string;
    country: string;
}