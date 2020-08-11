import { BASE_URL } from "./http.service";

/**
 * tryDecodeLink attempts to decode a link encoded in base64
 * If the link is not base64 format, it returns it unmodified.
 * If the link is encoded multiple times, it attempts to decode
 * until it finds a URL formatted string.
 * @param link to decode
 */
export function tryDecodeLink(link: string = "", tries = 3): string {
  let parsedLink = link;
  for (let i = 0; i < tries; i++) {
    try {
      parsedLink = atob(parsedLink);
    } catch (e) {
      break;
    }
    if (parsedLink.includes("http")) break;
  }
  const retVal = parsedLink.includes("http") ? parsedLink : link;
  return retVal.includes("http") ? retVal : null;
}

export function isValidHttpUrl(link: string) {
  let url: URL;

  try {
    url = new URL(link);
  } catch (_) {
    return false;
  }

  console.log(url);

  return (
    (url.protocol === "http:" || url.protocol === "https:") &&
    url.host === "mega.nz"
  );
}

export interface Download {
  fileName: string;
  startDate: Date;
  type: string;
  progress: number;
}
