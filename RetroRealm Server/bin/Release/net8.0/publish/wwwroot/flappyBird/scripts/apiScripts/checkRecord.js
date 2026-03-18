import scoreState from "../scoreNum.js";
import getRecord from "./getRecord.js";
import uploadRecord from "./uploadRecord.js";

export default async function checkRecord() {
    // Aktuális rekord lekérése
    const recordResponse = await getRecord();
    const status = scoreState.score;

    // Ha nem sikerült lekérni a rekordot
    if (!recordResponse.success) {
        return false;
    }

    const record = recordResponse.data.maxPassedPipes;

    // Új rekord esetén frissítés
    if (record < status) {
        await uploadRecord({ maxPassedPipes: status });
        return true;
    }

    // Nincs új rekord
    return false;
}
