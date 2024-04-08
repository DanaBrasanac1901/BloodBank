export class accessTokenModel{
    accessToken : string = ''

    public constructor(token? : string){
        if (token){
            this.accessToken = token;
        }
    }
}