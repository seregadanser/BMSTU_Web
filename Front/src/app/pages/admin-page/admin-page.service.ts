import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export class Place {
  constructor(
    public Id: number,
    public NumberStay: number,
    public NumberLayer: number,
    public Size: number
  ) { }
}

export class PlaceWI {
  constructor(
    public NumberStay: number,
    public NumberLayer: number,
    public Size: number
  ) { }
}

export class AdminCompose {
  constructor(
    public ProductId: number,
    public Name: string,
    public DateCome: string,
    public DateProduction: string,
    public InventoryNumber: number,
    public PlaceId: string
  ) { }
}


@Injectable({
  providedIn: 'root'
})
export class AdminPageService {

  private apiItemsUrl = 'http://localhost:5001/inventoryProducts';
  private apiPlacesUrl = "http://localhost:5001/places";

  private item_columns: string[] = ['InventoryNumber', "ProductId", "Name", "DateCome", "DateProduction", "PlaceId"];
  private place_columns: string[] = ["Id", "NumberStay", "NumberLayer", "Size"];

  private page: number = 1;
  private perPage: number = 2;

  constructor(private http: HttpClient) { }



  getItemColumns(): string[] {
    return this.item_columns;
  }

  getInventoryProducts(): Observable<{ items: AdminCompose[]; pages: number }> {
    const params = new HttpParams()
      .set('page', this.page ? this.page.toString() : '')
      .set('per_page', this.perPage ? this.perPage.toString() : '');

    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });

    return this.http.get(`${this.apiItemsUrl}`, { headers, params }).pipe(
      map((data: any) => ({
        items: data.results.map((ItemData: any) => new AdminCompose(
          ItemData.productId,
          ItemData.name,
          ItemData.dateCome,
          ItemData.dateProduction,
          ItemData.inventoryNumber,
          ItemData.placeId
        )),
        pages: Math.ceil(data.total / data.per_page),
      }))
    );
  }

  addInventoryProduct(productData: AdminCompose): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.post<AdminCompose>(`${this.apiItemsUrl}`, productData, { headers });
  }

  getInventoryProductById(id: number): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.get(`${this.apiItemsUrl}/${id}`, { headers });
  }

  deleteInventoryProductById(id: number): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.delete(`${this.apiItemsUrl}/${id}`, { headers });
  }



  getPlacesColumns(): string[] {
    return this.place_columns;
  }

  getPlaces(): Observable<{ items: Place[]; pages: number }> {
    const params = new HttpParams()
      .set('page', this.page ? this.page.toString() : '')
      .set('per_page', this.perPage ? this.perPage.toString() : '');

    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });

    return this.http.get(`${this.apiPlacesUrl}`, { headers, params }).pipe(
      map((data: any) => ({
        items: data.results.map((placeData: any) => new Place(
          placeData.id,
          placeData.numberStay,
          placeData.numberLayer,
          placeData.size
        )),
        pages: Math.ceil(data.total / data.per_page),
      }))
    );
  }

  addPlace(placeData: PlaceWI): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.post(`${this.apiPlacesUrl}`, placeData, { headers });
  }

  getPlaceById(id: number): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.get(`${this.apiPlacesUrl}/${id}`, { headers });
  }

  updatePlaceById(id: number, updatedPlaceData: Place): Observable<any> {
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.patch(`${this.apiPlacesUrl}/${id}`, updatedPlaceData, { headers });
  }

  deletePlaceById(id: number): Observable<any> {
    console.log("fff");
    const headers = new HttpHeaders({ "Authorization": "Bearer " + localStorage.getItem("id_token") });
    return this.http.delete(`${this.apiPlacesUrl}/${id}`, { headers });
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
