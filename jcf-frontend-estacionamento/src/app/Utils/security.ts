import { Usuario } from "../Models/usuario.model";

export class Security {
  public static set(user: Usuario, token: string) {
    const data = JSON.stringify(user);

    localStorage.setItem('jcf-estacionamento-user', btoa(data));
    localStorage.setItem('jcf-estacionamento-token', token);
  }

  public static setUser(user: Usuario) {
    const data = JSON.stringify(user);
    localStorage.setItem('jcf-estacionamento-user', btoa(data));
  }

  public static setToken(token: string) {
    localStorage.setItem('jcf-estacionamento-token', token);
  }

  public static getUser(): Usuario | null {
    const data = localStorage.getItem('jcf-estacionamento-user');
    if (data) {
      return JSON.parse(atob(data));
    }
    return null;
  }

  public static getToken(): string | null {
    const data = localStorage.getItem('jcf-estacionamento-token');
    if (data) {
      return data;
    }
    return null;
  }

  public static hasToken(): boolean {
    if (this.getToken())
      return true;
    else
      return false;
  }

  public static clear() {
    localStorage.removeItem('jcf-estacionamento-user');
    localStorage.removeItem('jcf-estacionamento-token');
  }
}
