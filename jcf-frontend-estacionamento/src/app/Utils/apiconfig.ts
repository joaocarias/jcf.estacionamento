export class Apiconfig {
    private static apiUrl = 'https://localhost:7020';  
    
    public static getApiUrl() {
       return this.apiUrl;
    }
}