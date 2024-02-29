import type { APIRoute } from "astro";

export const GET: APIRoute = async () => {
  console.log("-----1");
  const response = await fetch(`${process.env.SERVER ?? ""}/api/v1/users`);
  console.log("-----2");
  return new Response(JSON.stringify(response.json()));
};
