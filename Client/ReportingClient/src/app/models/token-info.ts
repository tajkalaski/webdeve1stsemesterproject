
export class TokenInfo {
    public token: string;
    public refreshToken: string;

    constructor(token: string, refreshToken: string) {
        this.token = token;
        this.refreshToken = refreshToken;
    }
}