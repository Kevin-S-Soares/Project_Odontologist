import * as crypto from "crypto";
import { Role } from "./enums";

export class JWTDetails {
  private _token = "";
  private _sub = "";
  private _isSignatureValid = false;
  private _isExpired = true;
  private _name = "";
  private _image = "";
  private _role: Role = Role.NONE;
  private _verified = "";

  public setToken(authorizationHeader: string) {
    const expression = /bearer\s([A-z0-9\.\-\_]+)/;
    const match = authorizationHeader.match(expression);
    if (match === null || match.length != 2) {
      return;
    }
    this._token = match[1];
    const [header64, payload64, signature64] = this._token.split(".");
    const payload = Buffer.from(payload64, "base64url").toString();
    const parsedPayload = JSON.parse(payload);
    this._sub = parsedPayload["sub"];
    this._name = parsedPayload["name"];
    this._image = parsedPayload["pic"];
    this._verified = parsedPayload["verified"];
    this._role = parseInt(parsedPayload["role"]) as Role;
    this._isExpired =
      new Date(parseInt(parsedPayload["exp"]) * 1000).getTime() <=
      new Date().getTime();
    /*
    const hmac = crypto.createHmac("sha512", process.env.SECRET_KEY ?? "");
    hmac.update(`${header64}.${payload64}`);
    this._isSignatureValid = hmac.digest("base64url") === signature64;
    */
    const verifier = crypto.createVerify("RSA-SHA512");
    verifier.update(`${header64}.${payload64}`);
    const parsedSignature = Buffer.from(signature64, "base64");
    this._isSignatureValid = verifier.verify(
      process.env.PUBLIC_KEY ?? "",
      parsedSignature,
    );
  }

  public resetToken() {
    this._token = "";
    this._isSignatureValid = false;
    this._isExpired = true;
    this._name = "";
    this._image = "";
    this._role = Role.NONE;
  }

  public getToken() {
    return this._token;
  }

  public isValid() {
    return !this._isExpired && this._isSignatureValid;
  }

  public getName() {
    return this._name;
  }

  public getImage() {
    return this._image;
  }

  public getRole() {
    return this._role;
  }

  public isVerified() {
    return this._verified === "";
  }

  public getId() {
    return this._sub;
  }
}
