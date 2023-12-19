import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export class WorkerLookCompose {
  constructor(
    public Inventory_number: number,
    public DateCome: string,
    public DateProduction: string,
    public Name: string,
    public DateOfStart: string
  ) {}
}

export class WorkerLookComposeWS {
  constructor(
    public Inventory_number: number,
    public DateCome: string,
    public DateProduction: string,
    public Name: string,
  ) {}
}

@Injectable({
  providedIn: 'root'
})
export class WorkerPageService {

  private apiFreeInventoryProductsUrl = 'http://localhost:5001/freeInventoryProducts'

  private item_columns: string[] = ["Inventory_number", "Name", "DateProduction","DateCome"];
  private own_columns: string[] = ["Inventory_number", "Name", "DateProduction","DateCome", "DateOfStart"];


  constructor(private http: HttpClient) { }
  private page : number = 1;
  private perPage: number = 2;

  getItemColumns(): string[] {
    return this.item_columns;
  }

  getItemFColumns(): string[] {
    return this.own_columns;
  }

  getFreeInventoryProducts(usingFilter: boolean): Observable<{items: WorkerLookCompose[]; pages: number}> {
    console.log(usingFilter);
    const params = new HttpParams()
      .set('page', this.page ? this.page.toString() : '')
      .set('per_page', this.perPage ? this.perPage.toString() : '')
      .set('using_flag', usingFilter.toString());

    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });

    return this.http.get(`${this.apiFreeInventoryProductsUrl}`, { headers, params }).pipe(
      map((data: any) => ({
        items: data.results.map((Data: any )=> new WorkerLookCompose(
          Data.inventory_number,
          Data.dateCome,
          Data.dateProduction,
          Data.name,
          Data.dateOfStart
        )),
        pages: Math.ceil(data.total/data.per_page),
      }))
    );
  }

  addFreeInventoryProduct(productData: WorkerLookComposeWS): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.post(`${this.apiFreeInventoryProductsUrl}`, productData, { headers });
  }

  getFreeInventoryProductById(id: string): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.get(`${this.apiFreeInventoryProductsUrl}/${id}`, { headers });
  }

  deleteFreeInventoryProductById(id: number): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.delete(`${this.apiFreeInventoryProductsUrl}/${id}`, { headers });
  }


  onGoTo(page: number): number {
    this.page = page;
    return this.page;
  }

  onNext(): number {
    this.page = this.page + 1;
    return this.page;
  }

  onPrevious(): number {
    this.page = this.page - 1;
    return this.page;
  }
}

