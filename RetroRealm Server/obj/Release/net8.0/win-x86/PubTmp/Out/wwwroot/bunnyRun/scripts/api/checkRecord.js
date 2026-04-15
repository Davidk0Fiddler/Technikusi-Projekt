import scoreState from "../scoreNum.js";
import GetRecord from "./GetRecord.js";
import UploadRecord from "./UploadRecord.js";

export default async function CheckRecord() {
  // Aktuális rekord lekérése
  const recordResponse = await GetRecord();
  const status = scoreState.score;

  // Ha nem sikerült lekérni a rekordot
  if (!recordResponse.success) {
    return false;
  }

  const record = recordResponse.data.maxDistance;
  // Új rekord esetén frissítés
  if (record < status) {
    await UploadRecord({ MaxDistance: status });
    return true;
  }

  // Nincs új rekord
  return false;
}
