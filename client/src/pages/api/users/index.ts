import type { APIRoute } from "astro";

export const GET: APIRoute = async () => {
  const response = await fetch(`${process.env.SERVER ?? ""}/api/v1/users`);
  return new Response(JSON.stringify(await response.json()));
};
